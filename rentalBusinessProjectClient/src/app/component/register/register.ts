import { Component, Inject, inject, signal } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../service/UserService';
import { UserCreate } from '../../../models/UserCreate';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { User } from '../../../models/User';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private fb = inject(FormBuilder);
  private userService = inject(UserService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  isLoading = signal(false);
  isEditMode = signal(false);


  ngOnInit() {
    const path = this.route.snapshot.url[0]?.path;
    this.isEditMode.set(path === 'edit-profile');

    if (this.isEditMode()) {
      const user = this.userService.currentUser();
      if (user) {
        // מילוי הטופס בנתונים הקיימים
        this.registerForm.patchValue({
          name: user.name,
          email: user.email,
          password: ''
        });
      }
    }
    else {
      const pendingData = this.userService.getPendingLogin();
      if (pendingData) {
        this.registerForm.patchValue({
          email: pendingData.email,
          password: pendingData.password
        });
      }
    }
  }

  registerForm: FormGroup = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });


  onSubmit() {
    if (this.registerForm.invalid || this.isLoading()) {
      alert('נא למלא את כל השדות בצורה תקינה.');
      return;
    }

    const userValue = this.userService.currentUser(); 
    
    this.isLoading.set(true);
    const formData = this.registerForm.value;

    if (this.isEditMode() && userValue) {
      this.userService.updateUser(userValue.id, { ...formData, id: userValue.id })
        .subscribe({
          next: (updatedUser) => {
            this.userService.setCurrentUser(updatedUser);
            alert('הפרופיל עודכן בהצלחה!');
            this.isLoading.set(false);
            this.router.navigate(['/']);
          },
          error: (err) => {
            console.error(err);
            alert('שגיאה בעדכון הנתונים');
            this.isLoading.set(false);
          }
        });
    }
    else {
      this.userService.createUser(formData).subscribe({
        next: (newUser: User) => {
          this.userService.setCurrentUser(newUser);
          alert(`ברוך הבא ${newUser.name}! נרשמת בהצלחה.`);
          this.isLoading.set(false);
          this.router.navigate(['/']);
        },
        error: (err) => {
          console.error('שגיאה בתהליך ההרשמה:', err);
          alert('כתובת המייל קיימת במערכת.');
          this.isLoading.set(false);
          this.router.navigate(['/login']);
        }
      });
    }
  }
}
