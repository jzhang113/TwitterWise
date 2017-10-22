import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'stream',
    templateUrl: './stream.component.html'
})
export class StreamComponent {
    public stream: Tweet;

    constructor(private http: Http, @Inject('BASE_URL') root: string) {
        http.get(root + "/api/stream/start").subscribe(result => {
            console.log(result);
        });
    }

    public getMessages() {
        this.http.get("/api/stream").subscribe(result => {
            this.stream = result.json();
        });
    }
}

interface Tweet {
    text: string;
    time: Date;
    author: string;
}
