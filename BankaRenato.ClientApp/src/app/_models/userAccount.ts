import { Card } from './card';

export interface UserAccount {
    id: number;
    currency: string;
    balance: number;
    cards?: Card[];
}
