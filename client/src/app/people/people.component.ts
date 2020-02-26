import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@aspnet/signalr'; 

export class Person {
  name: string;
  role: string;
}

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {

  private hubConnection: signalR.HubConnection;

  peopleFromHub: Person[];
  peopleFromController: Person[];
  hubConnected = false;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.connectHub();
  }
  
  getPeopleFromController(): void {
    this.http.get("https://localhost:5001/api/People").subscribe((people: Person[]) => this.peopleFromController = people);
  }

  addPersonToController(name: string, role: string): void {
    let person: Person = {name, role};
    console.log("Adding: " + JSON.stringify(person));
    this.http.post("https://localhost:5001/api/People", person).subscribe(result => console.log(JSON.stringify(result)));
  }

  getPeopleFromHub(): void {
    this.hubConnection.invoke("GetPeople")
      .then((response: Person[]) => this.peopleFromHub = response)
      .catch(err => console.error(err));
  }

  addPersonToHub(name: string, role: string): void {
    let person: Person = {name, role};
    this.hubConnection.invoke("AddPersonViaStrings", name, role);
  }

  addPersonToHubViaJson(name: string, role: string): void {
    let person: Person = {name, role};
    this.hubConnection.invoke("AddPersonJson", person);
  }

  sayHelloHub(): void {
    this.hubConnection.invoke("SayHello", "matthew")
      .then(response => console.log(JSON.stringify(response)))
      .catch(err => console.error(err));
  }

  connectHub(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
     . withUrl('https://localhost:5001/peoplehub').build();
    this.hubConnection
      .start()
      .then(() => {
        console.log("Connection started");
        this.hubConnected = true;
      })
      .catch(err => console.log('Error while trying to connected'));
  }

}
