import React from 'react'

export default class SoundCloud extends React.Component {
    render() {        
        const autoPlay = false;
        const color = '4b2c20'
        return(
            <iframe 
            height="600"
            scrolling="no"
            allow="autoplay" 
            src={`https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/playlists/1020652654&auto_play=${autoPlay}&hide_related=true&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=false&color=${color}`}/>
        )        
    }
}