import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Hotel } from '../models/Hotel';
import { HotelsService } from '../services/hotels.service';
import { FormArray, FormBuilder, Validators, FormGroup  } from '@angular/forms';
import { ReservedRoom } from '../models/ReservedRoom';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})
export class AdminPageComponent implements OnInit {
  notification:any = null; 
  constructor(private service: HotelsService, private router : Router, public fb: FormBuilder,private Aservice: AdminService) { }
  page:number = 0;
  hotel: Hotel = new Hotel("", "");
  roomsForms : FormArray = this.fb.array([]);
  roomsList = [];
  displayedColumns: string[] = ['id', 'roomId', 'startDate', 'endDate'];
  dataSource:ReservedRoom[] = [];
  ngOnInit(): void {
   this.hotel.id = this.Aservice.getHotelIdOfLoggedIn();
  
   this.service.getAllReservedRooms()
   .subscribe((data)=>{
    this.dataSource = <ReservedRoom[]>data;
 });
    this.service.getAllRoomsOfHotel(this.hotel.id).subscribe(
      res => {
        if (res == [])
          this.addRoomForm();
        else {
         
          //generate formarray as per the data received from BankAccont table
          (res as []).forEach((room: any) => {
            this.roomsForms.push(this.fb.group({
              id: [room.id],
              roomType: [room.roomType, Validators.required]
            }));
          });
        }
      }
    );
    
  }

saveHotel() {
    this.showNotification();
   this.page = 1;
    this.service.addNewHotel(this.hotel)
    .subscribe((data)=>{
      this.hotel = <Hotel>data;
      console.log(this.hotel);
    });
}

showNotification() {
      this.notification = { class: 'text-primary', message: 'Added!' };

}

addRoomForm(){
  this.roomsForms.push(this.fb.group({
    id: [0],
    roomType: ['', Validators.required],
  }));
}

recordSubmit(fg: FormGroup) {
  if (fg.value.id == 0)
    this.service.addNewRoom(this.hotel.id, fg.value).subscribe(
      (res: any) => {
      console.log("added");
        //this.showNotification('insert');
      });
  // else
  //   this.service.updateItems(fg.value).subscribe(
  //     (res: any) => {
  //       // this.showNotification('update');
  //     })
  //     ;
}

onDelete(id:any, i:any) {
  // if (id == 0)
  //   this.roomsForms.removeAt(i);
  // else if (confirm('Are you sure to delete this record ?'))
  //   this.service.deleteItem(id).subscribe(
  //     res => {
  //       this.roomsForms.removeAt(i);
  //      // this.showNotification('delete');
  //     });
}



}
