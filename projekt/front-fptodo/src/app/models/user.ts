import ToDoItem from "./todoitem";

export interface User {
  id: number;
  name: string;
  email: string;
  age: number;
  toDoItems: ToDoItem[];
}
export default User;