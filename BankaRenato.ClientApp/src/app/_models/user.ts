import { UserAccount } from './userAccount';

export interface User {
    id: number;
    username: string;
    email: string;
    firstName: string;
    lastName: string;
    accounts?: UserAccount[];
}
