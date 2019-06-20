import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpRequest, HttpEventType, HttpEvent } from "@angular/common/http";
import { FileInfo } from "../models/FileInfo";
import { FileConstraints } from "../models/FileConstraints";
import { Observable } from "rxjs";

@Injectable()
export class FilesService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getConstraints(): Observable<FileConstraints> {
    return this.http.get<FileConstraints>(this.baseUrl + 'api/Files/GetFilesConstraint');
  }

  getFiles(): Observable<FileInfo[]> {
    return this.http.get<FileInfo[]>(this.baseUrl + 'api/Files/GetFiles');
  }
  Upload(files, username): Observable<HttpEvent<{}>> {
    if (files.length === 0)
      return;
 
    const formData = new FormData();

    for (let file of files)
      formData.append(file.name, file);
    formData.append(username, 'username');

    const uploadReq = new HttpRequest('POST', this.baseUrl + 'api/Files/Upload', formData, {
      reportProgress: true,
    });

    return this.http.request(uploadReq);
  }
}
