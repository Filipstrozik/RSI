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

  ngOnInit(): void {
    if (this.item) {
      console.log(new Date());
      console.log(this.item.dueTime);
      console.log(new Date(this.item.dueTime));
      this.item.dueTime = new Date(this.item.dueTime);
    }
  }

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
        .subscribe((item: ToDoItem) => {
          if (item) {
            if (this.boardId !== item.board.id) {
              this.updatedToDoItem.emit(true);
            } else if (this.item.priority !== item.priority) {
              this.updatedToDoItem.emit(false);
            } else {
              this.item = item;
            }
          }
        });
    });
  }

  onDelete() {
    this.todoApiService.deleteToDoItem(this.item.id).subscribe(() => {
      this.updatedToDoItem.emit();
    });
  }
}
