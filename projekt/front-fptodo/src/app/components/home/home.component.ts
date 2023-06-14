import { Component } from '@angular/core';
import Board from 'src/app/models/board';
import { TodoapiService } from 'src/app/services/todoapi.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  boards: Board[] = [];
  
  constructor(private todoApiService: TodoapiService) { }

  ngOnInit(): void {
    this.todoApiService.getAllBoards().subscribe((boards: Board[]) => {
      this.boards = boards;
      console.log(this.boards);
    });
  }
  
}
