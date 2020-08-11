import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { User } from '../shared/user.model';
import { CommentService } from './comment.service';
import { Comment } from './comment.model';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit {

  public users: User[] = [];
  public user: User;
  public comments: Comment[] = [];
  public comment: Comment;

  constructor(private userService: UserService,private commentService: CommentService) { }

  ngOnInit(): void {
    this.initialize();
  }

  public async initialize(): Promise<void> {
    this.user = this.userService.currentUser;
    this.comment = new Comment('', this.user.id, new Date());
    this.users = await this.userService.get();
    this.comments = await this.commentService.get();
    return;
  }

  public getUserName(id: number): string {
    return this.users.find(user=>user.id===id)?.name;
  }

  public async submit(): Promise<void> {
    await this.commentService.post(this.comment);
    this.comment = new Comment('', this.user.id, new Date());
    this.comments = await this.commentService.get();
  }
}
