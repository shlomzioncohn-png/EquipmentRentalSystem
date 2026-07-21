import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Business } from '../../models/Business';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  private apiUrl = 'https://localhost:7020/api/business';
  http: HttpClient = inject(HttpClient);



  getAllBusinesses(): Observable<Business[]> {
    return this.http.get<Business[]>(this.apiUrl);
  }


  getBusinessById(id: string): Observable<Business> {
    return this.http.get<Business>(`${this.apiUrl}/${id}`);
  }

  

  getBusinessesByUserId(userId: string): Observable<Business[]> {
    return this.http.get<Business[]>(`${this.apiUrl}/user/${userId}`);
  }


  createBusiness(business: Business): Observable<Business> {
    return this.http.post<Business>(this.apiUrl, business);
  }


  updateBusiness(id: string, business: Business): Observable<Business> {
    return this.http.put<Business>(`${this.apiUrl}/${id}`, business);
  }


  deleteBusiness(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }


}