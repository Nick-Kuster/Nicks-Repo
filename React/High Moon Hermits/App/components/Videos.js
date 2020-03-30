import React from 'react'
import { Typography, Paper } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
    root: {
        display:'flex',
        flexGrow: 1,
        justifyContent: 'center',
        alignContent: 'center',
        opacity: .98
    },    
    paperHeader: {
        display: 'flex',
        flexGrow: 1,
        flexWrap: 'wrap',
        padding: theme.spacing(4),     
        margin: 'auto',
        alignContent: 'center',
        justifyContent: 'space-around',
        opacity: .98,
        background: '#ddd'
    },
    paper: {
        display: 'flex',
        flexGrow: 1,
        flexWrap: 'wrap',
        padding: theme.spacing(4),
        background:  theme.palette.primary.main,
        margin: 'auto',
        maxWidth: '100%',
        minHeight: '100%',
        width:'100%',
        alignContent: 'center',
        justifyContent: 'space-around'
    },
    videoContainer: {
        display: 'flex',
        flexGrow: 1,
        flexDirection: 'column',
        alignContent: 'center',
        justifyContent: 'space-around',
        alignItems: 'center'
    },
    videoHeader: {
        marginBottom: 75,
        maxHeight: 20
    },
    video: {
        marginBottom: 50
    },
    imageFit: {
        width: '100px',
        height: '100px',
        maxWidth: '100%',
        height: 'auto'
    }
}));

function Video ({ link, headerTitle}){
    const classes = useStyles();
    return(        
        <div className={classes.videoContainer}>  
       
        <iframe className={classes.video} 
                width='560'
                height='315' 
                src={link}
                frameBorder="0" 
                allow='autoplay; encrypted-media'
                allowFullScreen
        />  
        <Typography className={classes.videoHeader} variant='h2'>
                {headerTitle}
        </Typography> 
        </div>             
    )
}

function VideoGrid() {
    const classes = useStyles();
    const playlistId = 'PLLB2v7B6Fp2K6lPkCA06goPmN0_e_YDHr';
    const featuredVideoLink = 'https://www.youtube.com/embed/Y8DO47Ucp2g'

    return(
        <React.Fragment>
        <Paper className={classes.paperHeader}>            
            <img className={classes.imageFit} src='http://localhost:8080/app/images/JustHerman.png'/>
            <Typography variant='h1'>
                Videos
            </Typography>            
            <img className={classes.imageFit} src='http://localhost:8080/app/images/JustHerman.png'/>
        </Paper>
        <div className={classes.root}>
            <Paper className={classes.paper}>
                <Video link={featuredVideoLink} headerTitle='Featured'/>             
                <Video link={`https://www.youtube.com/embed/videoseries?list=${playlistId}`} headerTitle='Playlist'/>           
            </Paper>
        </div>
        </React.Fragment>
       
    )
}

export default class Videos extends React.Component{
    state = {
        folderPath: null,
        videos: [],
        error: null
    } 

    render(){
        // const {videos} = this.state      

        return (
            <React.Fragment>
                <VideoGrid />
            </React.Fragment>
        )
    }
}
