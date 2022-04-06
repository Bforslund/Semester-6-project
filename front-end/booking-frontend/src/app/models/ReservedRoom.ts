import { Room } from "./Room";

export class ReservedRoom{
    constructor(
        public id:number,
        public room: Room,
        public startDate:string,
        public endDate:string 
        ) {}

}