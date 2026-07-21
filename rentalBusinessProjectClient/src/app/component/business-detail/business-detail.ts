import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BusinessService } from '../../service/BusinessService';
import { Business } from '../../../models/Business';
import { Location } from '@angular/common';
import { UserService } from '../../service/UserService';

@Component({
  selector: 'app-business-detail',
  imports: [RouterLink],
  templateUrl: './business-detail.html',
  styleUrl: './business-detail.css',
})
export class BusinessDetail {
  private route = inject(ActivatedRoute);
  private businessService = inject(BusinessService);
  private location = inject(Location);
  userService = inject(UserService);

  business = signal<Business | null>(null);
  isLoading = signal<boolean>(true);

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.businessService.getBusinessById(id).subscribe((data: Business) => {
        this.business.set(data);
        this.isLoading.set(false);
      });
    }
  }

  goBack(): void {
    this.location.back();
  }

}
