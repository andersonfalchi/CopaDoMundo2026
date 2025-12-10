window.playVideo = function (videoElement) {
    if (videoElement) {
        videoElement.currentTime = 0; 
        videoElement.play();
    }
};
