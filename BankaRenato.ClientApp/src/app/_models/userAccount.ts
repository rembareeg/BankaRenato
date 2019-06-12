import { Card } from './card';

export interface UserAccount {
    id: number;
    name: string;
    currency: string;
    balance: number;
    cards?: Card[];
}
