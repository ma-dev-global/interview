import { Injectable } from '@angular/core';

import { User } from './user.model';

const NAMES: string[] = [
  'Charles Xavier',
  'Scott Summers',
  'Robert Drake',
  'Warren Worthington III',
  'Henry McCoy',
  'Jean Grey',
  'Anna-Marie LeBeau',
  'Lucas Bishop',
  'Elizabeth Braddock',
  'Ororo Monroe'
]

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public currentUser: User;

  private users: User[] = []
  
  constructor() {
    this.users = [];
    for (let index = 0; index < 10; index++) {
      const user = new User(index + 1, NAMES[Math.floor(Math.random() * 10)]);
      this.users.push(user);      
    }
    this.currentUser = this.users[Math.floor(Math.random() * 10)];
  }


  public async get(): Promise<User[]> {
    const promise = new Promise<User[]>((resolve, reject) => {
      setTimeout(()=>{
        resolve(this.users)
      }, 1000)
    });
    return promise;
  }
}
