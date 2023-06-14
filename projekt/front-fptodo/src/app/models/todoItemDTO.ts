export interface ToDoItemDTO {
  id?: number;
  name?: string;
  isComplete: boolean;
  dueTime?: Date;
  priority: number;
  boardId: number;
  userId?: number;
}

export default ToDoItemDTO;