import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ItemService} from '../../service/ItemService';
import { UserService } from '../../service/UserService';

@Component({
  selector: 'app-items-list',
  imports: [RouterLink],
  templateUrl: './items-list.html',
  styleUrl: './items-list.css',
})
export class ItemsList {
  private route = inject(ActivatedRoute);
  private itemService = inject(ItemService);
  public userService = inject(UserService);

  items = signal<any[]>([]);
  isLoading = signal(true);
  businessId = signal<string | null>(null);

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('businessId');
    this.businessId.set(id);

    if (id) {
      this.itemService.getItemsByBusinessId(id).subscribe({
        next: (data) => {
          this.items.set(data);
          this.isLoading.set(false);
        },
        error: () => this.isLoading.set(false)
      });
    }
  }

  deleteItem(itemId: string) {
    if (confirm('בטוח למחוק פריט זה?')) {
      this.itemService.deleteItems(itemId).subscribe({
        next: () => {
          this.items.update(prev => prev.filter(item => item.id !== itemId));
        },
        error: (err) => {
          console.error(err);
          alert('שגיאה במחיקת הפריט');
        }
      });
    }
  }
}