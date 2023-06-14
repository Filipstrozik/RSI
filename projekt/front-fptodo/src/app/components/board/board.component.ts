import { Component, Input } from '@angular/core';
import Board from 'src/app/models/board';
import { TodoapiService } from 'src/app/services/todoapi.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css'],
})
export class BoardComponent {
  @Input() board!: Board;

  constructor(private todoApiService: TodoapiService) {}

  updateBoard() {
    console.log('update board');
    this.todoApiService
      .getBoardById(this.board.id)
      .subscribe((board: Board) => {
        this.board = board;
      });
  }
}
