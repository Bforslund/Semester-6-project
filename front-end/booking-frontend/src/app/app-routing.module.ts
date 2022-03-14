import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HotelDetailsComponent} from './hotel-details/hotel-details.component';
import {HotelFrontpageComponent} from './hotel-frontpage/hotel-frontpage.component';
const routes: Routes = [
  {
  path: 'details/:id',
  component: HotelDetailsComponent
},
{
  path: '',
  component: HotelFrontpageComponent
},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
