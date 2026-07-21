import { Component, inject, signal } from '@angular/core';
import { BusinessService } from '../../service/BusinessService';
import { UserService } from '../../service/UserService';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-businesses',
  imports: [RouterLink],
  templateUrl: './my-businesses.html',
  styleUrl: './my-businesses.css',
})
export class MyBusinesses {
  private businessService = inject(BusinessService);
  private userService = inject(UserService);
  private route = inject(ActivatedRoute);

  myBusinesses = signal<any[]>([]);
  isLoading = signal(true);

  userId: string | null = null;

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('userId');
    this.userId = id;
    
    const user = this.userService.currentUser();
    if (user) {
      this.businessService.getBusinessesByUserId(user.id).subscribe({
        next: (data) => {
          this.myBusinesses.set(data);
          this.isLoading.set(false);
        },
        error: () => this.isLoading.set(false)
      });
    }
  }

  deleteBusiness(businessId: string) {
    if (confirm('בטוח למחוק עסק זה?')) {
      this.businessService.deleteBusiness(businessId).subscribe({
        next: () => {
          this.myBusinesses.update(b => b.filter(b => b.id !== businessId));
        },
        error: (err) => {
          console.error(err);
          alert('יש לגמ"ח שאתה רוצה למחוק ציוד פעיל  אנא מחק את הציוד לפני מחיקת העסק');
        }
      })
    }
  }
}

