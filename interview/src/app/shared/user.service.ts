import { Injectable } from '@angular/core';

import { User } from './user.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  public currentUser: User;

  private users: User[] = [
    { id: 6363, name: 'Lucas Bishop' },
    { id: 1000, name: 'Jean Grey' },
    { id: 3000, name: 'Robert Drake' }
  ]

  constructor() {
    this.currentUser = this.users[Math.floor(Math.random() * this.users.length)];
  }

  public async get(): Promise<User[]> {
    const promise = new Promise<User[]>((resolve, reject) => {
      setTimeout(() => {
        resolve(this.users)
      }, 50)
    });
    return promise;
  }
}















/* 
  Test Data Below
*/

class FakeService {

  private NAMES: string[] = [
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

  private users: User[];
  private initialize() {
    this.users = [];
    for (let index = 0; index < 10; index++) {
      const user = new User(6363, this.NAMES[Math.floor(Math.random() * 10)]);
      this.users.push(user);
    }
  }
}