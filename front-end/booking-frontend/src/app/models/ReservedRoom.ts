import { Room } from "./Room";

export class ReservedRoom{
    constructor(
        public id:number,
        public roomId: number,
        public startDate:string,
        public endDate:string 
        ) {}

}