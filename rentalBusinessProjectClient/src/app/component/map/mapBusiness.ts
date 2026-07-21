declare var google: any;
import { Component, ElementRef, Input, SimpleChanges, ViewChild } from '@angular/core';

@Component({
  selector: 'app-business-map',
  imports: [],
  templateUrl: './mapBusiness.html',
  styleUrl: './mapBusiness.css',
})
export class mapBusiness {
  @ViewChild('mapContainer') mapElement!: ElementRef;

  @Input() lat: any = 0;
  @Input() lng: any = 0;
  @Input() businessName: string = '';

  ngAfterViewInit(): void {
    this.initGoogleMap();
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (this.lat !== 0 && this.lng !== 0) {
      this.initGoogleMap();
    }
  }

  initGoogleMap() {
    if (!this.mapElement || !this.mapElement.nativeElement) {
      console.log("המפה עדיין לא מוכנה ב-DOM");
      return;
    }
    if (this.lat === 0 || this.lng === 0) return;
    const coordinates = new google.maps.LatLng(this.lat, this.lng);

    // 2. הגדרות המפה
    const mapOptions: any = {
      center: coordinates,
      zoom: 16, // זום לרמת רחוב
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    // 3. יצירת המפה בפועל בתוך האלמנט שתפסנו
    const map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);

    // 4. הוספת הסיכה האדומה (Marker)
    new google.maps.Marker({
      position: coordinates,
      map: map,
      title: this.businessName, // כשעומדים על הסיכה רואים את שם הגמ"ח
      animation: google.maps.Animation.DROP // אפקט של נפילה כשזה נטען
    });
  }


}
