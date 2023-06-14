export interface ToDoItem {
  id?: number;
  name?: string;
  isComplete: boolean;
  dueTime?: Date;
  priority: number;
  boardId: number;
  userId?: number;
}

export default ToDoItem;