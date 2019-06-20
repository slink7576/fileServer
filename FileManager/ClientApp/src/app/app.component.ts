import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';

import { FilesService } from './services/files.service';
import { FileInfo } from './models/fileInfo';
import { FileConstraints } from './models/fileConstraints';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'app';
  files: Array<FileInfo>;
  constraints: FileConstraints;
  status = '';
  username = '';
  constructor(private http: HttpClient, private filesService: FilesService) { }

  onFileUpload(files) {
    if (this.username.length == 0) {
      this.status = 'Please enter your name';
      return;
    }
    if (files[0].size > this.constraints.size) {
      this.status = 'File is too big!';
      return;
    }
    this.filesService.Upload(files, this.username).subscribe(event => {
      if (event.type == HttpEventType.UploadProgress) {
        this.status = 'Loading...';
      }
      else if (event.type == HttpEventType.Response) {
        this.status = event.body.toString();
        this.filesService.getFiles().subscribe(data => {
          this.files = data;
        });
      }
    });

  }

  ngOnInit() {
    this.filesService.getFiles().subscribe(data => {
      this.files = data;
    });
    this.filesService.getConstraints().subscribe(data => this.constraints = data);
  }

}



