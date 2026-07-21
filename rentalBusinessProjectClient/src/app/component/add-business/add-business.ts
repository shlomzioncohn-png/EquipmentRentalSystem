import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BusinessService } from '../../service/BusinessService';
import { UserService } from '../../service/UserService';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-business',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-business.html',
  styleUrl: './add-business.css'
})
export class AddBusiness {
  private fb = inject(FormBuilder);
  private businessService = inject(BusinessService);
  private userService = inject(UserService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  private selectedAddress: { lat: number, lng: number } | null = null;

  isLoading = signal(false);
  isEditMode: boolean = false;
  businessId: string | null = null;


  businessForm: FormGroup = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(2)]],
    city: ['', Validators.required],
    neighborhood: ['', Validators.required],
    street: ['', Validators.required],
    houseNumber: ['', Validators.required],
    phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
    openTime: ['', Validators.required],
    closeTime: ['', Validators.required],
    comments: ['']
  });

  ngOnInit() {
    const firstSegment = this.route.snapshot.url[0]?.path;
    this.isEditMode = firstSegment === 'edit-business';

    if (this.isEditMode) {
      this.businessId = this.route.snapshot.paramMap.get('businessId');
      if (this.businessId) {
        this.businessService.getBusinessById(this.businessId).subscribe(business => this.businessForm.patchValue(business));
      }
    }
  }

  ngAfterViewInit() {
    const input = document.getElementById('address-input') as HTMLInputElement;
    const autocomplete = new google.maps.places.Autocomplete(input, {
      componentRestrictions: { country: 'il' }
    });
    autocomplete.addListener('place_changed', () => {
      const place = autocomplete.getPlace();
      console.log("Full Place Object:", place);
      if (place.geometry && place.geometry.location) {
        this.selectedAddress = {
          lat: place.geometry.location.lat(),
          lng: place.geometry.location.lng()
        };
        let city='', neighborhood='', street='', houseNumber='';
        place.address_components?.forEach(component => {
          if (component.types.includes('locality')) city = component.long_name;
          if (component.types.includes('sublocality') || component.types.includes("neighborhood")) neighborhood = component.long_name;
          if (component.types.includes('route')) street = component.long_name;
          if (component.types.includes('street_number')) houseNumber = component.long_name;
        })
        this.businessForm.patchValue({ city, neighborhood, street, houseNumber });
      }
    })
  }


  onSubmit() {
    if (this.businessForm.valid && this.selectedAddress) {
      const currentUser = this.userService.currentUser();

      if (!currentUser) {
        alert('עליך להיות מחובר כדי להוסיף עסק');
        this.router.navigate(['/login']);
        return;
      }

      this.isLoading.set(true);

      const businessData = {
        ...this.businessForm.value,
        userId: currentUser.id,
        ownerName: currentUser.name,
        email: currentUser.email,

        latitude: this.selectedAddress?.lat,
        longitude: this.selectedAddress?.lng,
      };
      const action = this.isEditMode && this.businessId ?
        this.businessService.updateBusiness(this.businessId, businessData) :
        this.businessService.createBusiness(businessData);

      action.subscribe({
        next: () => {
          this.isEditMode ? alert('הגמ"ח עודכן בהצלחה!')
            : alert('הגמ"ח נוסף בהצלחה!');
          this.router.navigate(['/business-list']);
        },
        error: (err) => {
          console.error('שגיאה בשמירת הנתונים:', err);
          alert('אופס! משהו השתבש בשמירת הנתונים.');
          this.isLoading.set(false);
        }
      });
    }
  }
}
