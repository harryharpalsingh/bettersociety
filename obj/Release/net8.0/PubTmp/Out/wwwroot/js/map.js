var mymap = L.map('map', {
    center: [51.505, -0.09],
    zoom: 13
});

L.tileLayer('../../../../../../tile.openstreetmap.org/%7bz%7d/%7bx%7d/%7by%7d.html', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(mymap);

L.marker([51.5, -0.09]).addTo(mymap)
    .bindPopup("<b>Hello World!</b><br/>I am a popup.")