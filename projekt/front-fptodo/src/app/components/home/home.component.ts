import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import Board from 'src/app/models/board';
import { TodoapiService } from 'src/app/services/todoapi.service';
import { BoardAddComponent } from '../board/board-add/board-add.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  boards: Board[] = [];

  constructor(
    private todoApiService: TodoapiService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.updateBoards();
  }

  updateBoards() {
    this.todoApiService.getAllBoards().subscribe((boards: Board[]) => {
      this.boards = boards;
      console.log(this.boards);
    });
  }

  onAddBoard() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = null;
    this.dialog
      .open(BoardAddComponent, dialogConfig)
      .afterClosed()
      .subscribe((item: any) => {
        this.updateBoards();
      });
  }
}
