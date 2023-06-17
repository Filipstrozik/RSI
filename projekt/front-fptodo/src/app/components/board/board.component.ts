import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
export class BoardComponent implements OnInit {
  @Input() board!: Board;
  boardItemCount: number = 0;
  boardPrioritySum: number = 0;
  @Output() updatedBoard: EventEmitter<any> = new EventEmitter();

  constructor(
    private todoApiService: TodoapiService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.fetchBoardItemsCount();
    this.fetchBoardPrioritySum();
  }

  fetchBoardItemsCount() {
    this.todoApiService
      .getBoardItemsCountById(this.board.id!)
      .subscribe((count) => {
        this.boardItemCount = count;
      });
  }

  fetchBoardPrioritySum() {
    this.todoApiService
      .getBoardPriorityById(this.board.id!)
      .subscribe((sum) => {
        this.boardPrioritySum = sum;
      });
  }

  updateBoard(event: any) {
    this.todoApiService
      .getBoardById(this.board.id!)
      .subscribe((board: Board) => {
        if (event) {
          this.updatedBoard.emit();
        } else {
          this.board = board;
          this.fetchBoardItemsCount();
          this.fetchBoardPrioritySum();
        }
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
        this.updateBoard(false);
      });
  }

  onDeleteBoard() {
    this.todoApiService.deleteBoard(this.board.id!).subscribe(() => {
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
        this.updateBoard(false);
      });
  }
}
