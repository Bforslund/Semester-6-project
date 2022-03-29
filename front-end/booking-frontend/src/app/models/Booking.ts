import { Hotel } from "./Hotel";

export class Booking{
    constructor(
        public id:number,
        public hotel: Hotel,
        public ContactInfo: string,
        public startTime:string,
        public endTime:string 
    ) { 
       
     }

}