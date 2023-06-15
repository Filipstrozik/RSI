import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import Board from 'src/app/models/board';
import { TodoapiService } from 'src/app/services/todoapi.service';
import { ToDoItemAddComponent } from '../to-do-item/to-do-item-add/to-do-item-add.component';
import { BoardAddComponent } from './board-add/board-add.component';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css'],
})
export class BoardComponent {
  @Input() board!: Board;
  @Output() updatedBoard: EventEmitter<any> = new EventEmitter();

  constructor(
    private todoApiService: TodoapiService,
    private dialog: MatDialog
  ) {}

  updateBoard() {
    this.todoApiService
      .getBoardById(this.board.id!)
      .subscribe((board: Board) => {
        this.board = board;
        this.updatedBoard.emit();
      });
  }

  onAddToDoItem() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = this.board;
    this.dialog
      .open(ToDoItemAddComponent, dialogConfig)
      .afterClosed()
      .subscribe((item: any) => {
        console.log('item', item);
        this.updateBoard();
      });
  }

  onDeleteBoard() {
    this.todoApiService.deleteBoard(this.board.id!).subscribe(() => {
      console.log('deleted');
      this.updatedBoard.emit();
    });
  }

  onEditBoard() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = this.board;
    this.dialog
      .open(BoardAddComponent, dialogConfig)
      .afterClosed()
      .subscribe((item: any) => {
        this.updateBoard();
      });
  }
}
