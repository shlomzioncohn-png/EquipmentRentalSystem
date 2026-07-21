import { Routes } from '@angular/router';
import { Home } from './component/home/home';
import { Register } from './component/register/register';
import { Login } from './component/login/login';
import { MyBusinesses } from './component/my-businesses/my-businesses';
import { BusinessDetail } from './component/business-detail/business-detail';
import { BusinessList } from './component/business-list/business-list';
import { AddBusiness } from './component/add-business/add-business';
import { AddItem } from './component/add-item/add-item';
import { ItemsList } from './component/items-list/items-list';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'edit-profile', component: Register }, //   עריכת USER

  { path: 'my-businesses', component: MyBusinesses },
  { path: 'business-detail/:id', component: BusinessDetail },
  { path: 'businesses', component: BusinessList },
  { path: 'add-business', component: AddBusiness },//הוספת גמ"ח
  { path: 'edit-business/:businessId', component: AddBusiness },//עריכת גמ"ח

  { path: 'add-item/:businessId', component: AddItem },//הוספת פריט
  { path: 'item-list/:businessId', component: ItemsList },
  { path: 'edit-item/:businessId/:itemId', component: AddItem },//עריכת פריט
  { path: '**', redirectTo: '' }
];
