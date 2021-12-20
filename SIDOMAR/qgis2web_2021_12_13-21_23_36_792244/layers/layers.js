var wms_layers = [];


        var lyr_OSMStandard_0 = new ol.layer.Tile({
            'title': 'OSM Standard',
            'type': 'base',
            'opacity': 1.000000,
            
            
            source: new ol.source.XYZ({
    attributions: ' &middot; <a href="https://www.openstreetmap.org/copyright">© OpenStreetMap contributors, CC-BY-SA</a>',
                url: 'http://tile.openstreetmap.org/{z}/{x}/{y}.png'
            })
        });
var format_tblgerai_1 = new ol.format.GeoJSON();
var features_tblgerai_1 = format_tblgerai_1.readFeatures(json_tblgerai_1, 
            {dataProjection: 'EPSG:4326', featureProjection: 'EPSG:3857'});
var jsonSource_tblgerai_1 = new ol.source.Vector({
    attributions: ' ',
});
jsonSource_tblgerai_1.addFeatures(features_tblgerai_1);
var lyr_tblgerai_1 = new ol.layer.Vector({
                declutter: true,
                source:jsonSource_tblgerai_1, 
                style: style_tblgerai_1,
                interactive: true,
                title: '<img src="styles/legend/tblgerai_1.png" /> tblgerai'
            });

lyr_OSMStandard_0.setVisible(true);lyr_tblgerai_1.setVisible(true);
var layersList = [lyr_OSMStandard_0,lyr_tblgerai_1];
lyr_tblgerai_1.set('fieldAliases', {'geraiid': 'geraiid', 'notelp': 'notelp', 'namagerai': 'namagerai', 'alamat': 'alamat', });
lyr_tblgerai_1.set('fieldImages', {'geraiid': '', 'notelp': '', 'namagerai': '', 'alamat': '', });
lyr_tblgerai_1.set('fieldLabels', {'geraiid': 'no label', 'notelp': 'header label', 'namagerai': 'header label', 'alamat': 'inline label', });
lyr_tblgerai_1.on('precompose', function(evt) {
    evt.context.globalCompositeOperation = 'normal';
});