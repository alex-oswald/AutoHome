var video = document.getElementById('video');
var videoSrc = 'https://wyze.pi.lan:8888/deck/stream.m3u8';
//
// First check for native browser HLS support
//
if (video.canPlayType('application/vnd.apple.mpegurl')) {
    video.src = videoSrc;
    //
    // If no native HLS support, check if HLS.js is supported
    //
} else if (Hls.isSupported()) {
    var hls = new Hls();
    hls.loadSource(videoSrc);
    hls.attachMedia(video);
}