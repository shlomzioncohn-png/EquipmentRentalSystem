import { Component, Inject, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BusinessService } from '../../service/BusinessService';
import { Business } from '../../../models/Business';
import { Router, RouterLink } from '@angular/router';
import { mapBusiness } from '../map/mapBusiness';

@Component({
  selector: 'app-business-list',
  standalone: true,
  imports: [CommonModule, RouterLink, mapBusiness],
  templateUrl: './business-list.html',
  styleUrl: './business-list.css'
})
export class BusinessList implements OnInit {
  isLoading = signal<boolean>(true);
  private businessService = inject(BusinessService);
  private router = inject(Router);
  businesses = signal<Business[]>([]);

  allBusinesses: Business[] = [];
  activeMapId: string | null = (null);

  currentCitySearch: string = '';
  currentNameSearch: string = '';

  ngOnInit(): void {
    this.businessService.getAllBusinesses().subscribe({
      next: (data) => {
        this.allBusinesses = data
        this.businesses.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('שגיאה בטעינת עסקים:', err);
        this.isLoading.set(false);
      }
    });
  }

  filterBusinesses() {
    const city = this.currentCitySearch.toLowerCase().trim();
    const name = this.currentNameSearch.toLowerCase().trim();
    const filterd = this.allBusinesses.filter(b => {
      const matchCity = city === '' || b.city?.toLocaleLowerCase().includes(city)
      const matchName = name === '' || b.name?.toLocaleLowerCase().includes(name)
      return matchCity && matchName
    })
    this.businesses.set(filterd)
  }

  toggleMap(businessId: string) {
    if (this.activeMapId === businessId)
      this.activeMapId = null;
    else
      this.activeMapId = businessId;
  }

  goToDetails(id: string) {
    this.router.navigate(['/business-detail', id]);
  }

  onCityInput(event: any) {
    this.currentCitySearch = event.target.value;
    this.filterBusinesses();
  }

  onNameInput(event: any) {
    this.currentNameSearch = event.target.value;
    this.filterBusinesses();
  }

}