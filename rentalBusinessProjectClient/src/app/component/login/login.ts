import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../service/UserService';
import { LoginRequest } from '../../../models/LoginRequest';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { User } from '../../../models/User';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private fb = inject(FormBuilder);
   userService = inject(UserService);
  private router = inject(Router);
  isLoading = signal(false);


  loginForm: FormGroup = this.fb.group({
    password: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]]
  });

  ngOnInit() {
    const currentUser = this.userService.currentUser();
    if (currentUser) {
      this.loginForm.patchValue(currentUser);
    }
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading.set(true);
      const loginRequest: LoginRequest = this.loginForm.value;
      console.log('שולח נתונים לשרת', loginRequest);
      this.userService.login(loginRequest).subscribe
        ({
          next: (user) => {
            this.userService.setCurrentUser(user);
            console.log('התחברת בהצלחה!', user);
            alert('התחברת בהצלחה!');
            this.isLoading.set(false);

            this.router.navigate(['/']);
          },
          error: (err) => {
            const goToRegister = confirm('לא מצאנו משתמש כזה. לעבור להרשמה?');
            this.isLoading.set(false);

            if (goToRegister) {
              this.userService.setPendingLogin(this.loginForm.value);
              this.router.navigate(['/register']);
            } else {
              this.loginForm.reset();
            }
          }
        })
    }
    else {
      alert('נא למלא את כל השדות בצורה תקינה לפני השליחה.');
    }
  }

  deleteUser() {
    const user = this.userService.currentUser();
    if (user) {
      const confirmDelete = confirm('האם אתה בטוח שאתה רוצה למחוק את החשבון לצמיתות?')
      if (confirmDelete) {
        this.userService.deleteUser(user.id).subscribe({
          next: () => {
            alert('החשבון נמחק בהצלחה');
            this.userService.logout();
            this.router.navigate(['/']);
          },
          error: (err) => {
            console.error(err);
            alert('יש גמח"ים פעילים במשתמש זה מחק אותם כדי לצאת מהמערכת לצמיתות');
          }
        });
      }
    } else {
      alert('אין משתמש מחובר כרגע.');
    }
  }

  logout() {
    this.userService.logout();
    this.loginForm.reset();
    this.router.navigate(['/']);
  }


}
