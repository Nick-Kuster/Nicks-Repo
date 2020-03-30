
const API_KEY = ''//process.env.YOUTUBE_API_KEY;
const channelId = '';

export function getVideos (){
    return fetch(`https://www.googleapis.com/youtube/v3/search?key=${API_KEY}&channelId=${channelId}&part=snippet,id&order=date&maxResults=50`)
        .then((res) => res.json())        
        .then((data) => data.items)
}