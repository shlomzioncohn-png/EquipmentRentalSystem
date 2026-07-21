import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Item } from '../../models/Item';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private apiUrl = 'https://localhost:7020/api/item';
    http:HttpClient=inject(HttpClient);
 
  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.apiUrl);
  }

  
  getItemsById(id: string): Observable<Item> {
    return this.http.get<Item>(`${this.apiUrl}/${id}`);
  }

 
  createItem(item: Item): Observable<Item> {
    return this.http.post<Item>(this.apiUrl, item);
  }

  
  deleteItems(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  
  updateItems(id: string, item: Item): Observable<Item> {
    return this.http.put<Item>(`${this.apiUrl}/${id}`, item);
  }

 
  getItemsByBusinessId(businessId: string): Observable<Item[]> {
    return this.http.get<Item[]>(`${this.apiUrl}/business/${businessId}`);
  }
}