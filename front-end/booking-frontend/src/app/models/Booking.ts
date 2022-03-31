import { Hotel } from "./Hotel";
import { Room } from "./Room";

export class Booking{
    constructor(
        public Id:number,
        public HotelId: number,
        public RoomType: Room,
        public ContactInfo: string,
        public Start:string,
        public End:string 
    ) { 
       
     }

}