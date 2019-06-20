import { DebugElement } from "@angular/core";
import { AppComponent } from "./app.component";
import { ComponentFixture, async, TestBed } from "@angular/core/testing";
import { By, BrowserModule } from '@angular/platform-browser';
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from '@angular/common/http';
import { FilesService } from "./services/files.service";


describe('AppComponent', function () {
  let de: DebugElement;
  let comp: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
      ],
      providers: [FilesService, { provide: 'BASE_URL', useValue: "http://example.com/api" }],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    comp = fixture.componentInstance;
  });

  it('Creates app component', () => expect(comp).toBeDefined());
  it('Upload file without username shows an error', () => {
    comp.username = '';
    comp.onFileUpload(null);
    expect(comp.status == 'Please enter your name');
  })
 
});
