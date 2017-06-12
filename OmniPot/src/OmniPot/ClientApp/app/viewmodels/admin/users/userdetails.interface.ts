//export interface UserDetailsViewModel {   
 
  //  public firstname: string;
//    public middlename: string;
  //  public lastname: string;
//    public dateofbirth: string;

//}

export interface User {
  name: string;
  address?: {
    street?: string;
    postcode?: string;
  }
}
