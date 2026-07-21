import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { BusinessService } from '../../service/BusinessService';
import { UserService } from '../../service/UserService';
import { ItemService } from '../../service/ItemService';

@Component({
  selector: 'app-add-item',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './add-item.html',
  styleUrl: './add-item.css'
})
export class AddItem implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private itemService = inject(ItemService);
  private businessService = inject(BusinessService);
  userService = inject(UserService);
  isLoading = signal(false);
  isEditMode = signal(false);
  currentBusiness = signal<any>(null);
  itemId: string | null = null;


  itemForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    amount: ['', Validators.required],
    price: [0, [Validators.min(0)]],
    isReturnable: [true, [Validators.required]],
    comments: ['']

  });

  ngOnInit() {
    const firstSegment = this.route.snapshot.url[0]?.path;
    this.isEditMode.set(firstSegment === 'edit-item');

    if (this.isEditMode()) {
      this.itemId = this.route.snapshot.paramMap.get('itemId');

      if (this.itemId) {
        this.itemService.getItemsById(this.itemId).subscribe(item => this.itemForm.patchValue(item));
      }
    }
    const bId = this.route.snapshot.paramMap.get('businessId');
    if (bId) {
      this.businessService.getBusinessById(bId).subscribe({
        next: (b) => {
          console.log('Business data received:', b); // הדפסה לצורך בדיקה
          this.currentBusiness.set(b);
        },
        error: (err) => {
          console.error('Failed to fetch business:', err);
        }
      });
    }

  }

  onSubmit() {
    if (this.itemForm.invalid || !this.currentBusiness()) return;
    this.isLoading.set(true);
    const business = this.currentBusiness();

    const data = {
      ...this.itemForm.value,
      businessId: business.id,
      businessName: business.name,
      businessCity: business.city,
    };
    const action = this.isEditMode() && this.itemId ?
      this.itemService.updateItems(this.itemId, data) :
      this.itemService.createItem(data);

    action.subscribe({
      next: () => {
        this.isEditMode() ? alert('הפריט עודכן בהצלחה!')
          : alert('הפריט נוסף בהצלחה!');
        this.router.navigate(['/item-list', business.id]);
      },
      error: (err) => {
        console.error(err);
        alert('שגיאה בשמירת הנתונים');
        this.isLoading.set(false);
      }
    });
  }
}
