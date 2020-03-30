import React from 'react'
import { makeStyles } from '@material-ui/core/styles';
import { IconButton, Tooltip, Paper } from '@material-ui/core';
import Quote from './Quote'
import FacebookIcon from '@material-ui/icons/Facebook';
import YouTubeIcon from '@material-ui/icons/YouTube';
import InstagramIcon from '@material-ui/icons/Instagram';
import TwitterIcon from '@material-ui/icons/Twitter';

const useStyles = makeStyles(theme => ({
    banner: { 
        display: 'flex',
        justifyContent: 'space-around',
        opacity:.98,
        alignItems: 'center',
        height: '15%',
        padding: '12px',
        borderRadius: '3px',
        background:  '#ddd',
    },
    imageContainer:{
        flex: 1,
        height: '100%',
        width: '100%',
        objectFit: 'contain'
    },
    image: {
        width: '100%',
        height: '100%',
        objectFit: 'contain'
    },
    quote: {
        flex: 1,
        marginTop: '20px'
    },
    socialMedia: {            
        display: 'flex',    
        flex: 1,
        justifyContent: 'space-around',
        alignItems: 'flex-End'
    },
}));
  
export default function Banner({children}) {
    const classes = useStyles();
    return(
        <Paper className={classes.banner}>    
        <div className={classes.quote}>
            <Quote />
        </div>
        <div className={classes.imageContainer}>
            <img  className={classes.image}src='http://localhost:8080/app/images/ShadowTransparentBg.png'/>   
        </div>
        <div className={classes.socialMedia}>
            <IconButton>
                <Tooltip title="Facebook"> 
                    <FacebookIcon  alt='FacebookIcon'/>
                </Tooltip>
            </IconButton>
            <IconButton>
                <Tooltip title="YouTube"> 
                    <YouTubeIcon  alt='YouTubeIcon'/>
                </Tooltip>
            </IconButton>
            <IconButton>
                <Tooltip title="Instagram"> 
                    <InstagramIcon alt='InstagramIcon'/>
                </Tooltip>
            </IconButton>
            <IconButton>
                <Tooltip title="Twitter"> 
                    <TwitterIcon alt='TwitterIcon'/>
                </Tooltip>
            </IconButton>
        </div>   
        </Paper>
    )
}