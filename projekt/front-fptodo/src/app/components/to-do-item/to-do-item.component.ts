import { Component, Input, Inject, Output, EventEmitter } from '@angular/core';
import ToDoItemDTO from 'src/app/models/todoItemDTO';
import ToDoItem from 'src/app/models/todoitem';
import { TodoapiService } from 'src/app/services/todoapi.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ToDoItemEditDialogComponent } from './to-do-item-edit-dialog/to-do-item-edit-dialog.component';

@Component({
  selector: 'app-to-do-item',
  templateUrl: './to-do-item.component.html',
  styleUrls: ['./to-do-item.component.css'],
})
export class ToDoItemComponent {
  @Input() item!: ToDoItem;
  @Input() boardId: number = 0;
  checked: boolean = false;

  @Output() updatedToDoItem: EventEmitter<any> = new EventEmitter();

  constructor(
    private todoApiService: TodoapiService,
    private dialog: MatDialog
  ) {}

  changeComplete() {
    // change to DTO
    const itemDTO: ToDoItemDTO = {
      id: this.item.id,
      name: this.item.name,
      isComplete: this.item.isComplete,
      dueTime: this.item.dueTime,
      priority: this.item.priority,
      boardId: this.boardId,
      userId: this.item.user?.id,
    };
    this.todoApiService.updateToDoItem(itemDTO).subscribe((item: ToDoItem) => {
      this.todoApiService
        .getToDoItemById(this.item.id)
        .subscribe((item: ToDoItem) => {
          this.item = item;
        });
    });
  }

  onEdit() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    this.todoApiService.getToDoItemById(this.item.id).subscribe((item) => {
      dialogConfig.data = item;
      this.dialog
        .open(ToDoItemEditDialogComponent, dialogConfig)
        .afterClosed()
        .subscribe((item: ToDoItemDTO) => {
          if (item) {
            // if user id changed, get user
            // if board id changed, emit event to update board
            if (this.item.user?.id !== item.userId) {
              // ser can bu unasigned
              this.todoApiService
                .getUserById(item.userId!)
                .subscribe((user) => {
                  this.item.user = user;
                });
            }
            if (this.boardId !== item.boardId) {
              this.updatedToDoItem.emit();
            }
          }
        });
    });
  }
}
