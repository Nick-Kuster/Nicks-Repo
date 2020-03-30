import React from 'react';
import { Link } from 'react-router-dom';
import clsx from 'clsx';
import { AppBar, Toolbar, Typography, IconButton, Avatar, Drawer } from '@material-ui/core';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import SoundCloud from './SoundCloud';
import QueueMusicIcon from '@material-ui/icons/QueueMusic';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';

const useStyles = makeStyles(theme => ({
    large: {
        width: theme.spacing(7),
        height: theme.spacing(7),
        marginRight: theme.spacing(2),
    },
    title: {
        flexGrow: 2
    },
    appBar:{
        marginBottom:'0px',
        background: theme.palette.primary.dark,
        opacity: .85,
        '& h6': {
            marginRight: '50'
        }
    },
    menu: {
        display: 'flex',
        alignContent: 'center',
        justifyContent: 'center',
        background: theme.palette.primary.light,
    },
    title: {
        flexGrow: 1,
    },
    drawerPaper: {
        background: '#ddd'
    }
}));


export default function Nav (){
    const classes = useStyles();
    const [open, setOpen] = React.useState(false);
    const theme = useTheme();

    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };    

    return(
        <React.Fragment>                    
            <AppBar className={classes.appBar} position='fixed'>
                <Toolbar >
                    <IconButton edge='start' className={classes.large} color='inherit' aria-label='menu'>
                        <Avatar alt='Herman'  src='app/images/JustHermanWhiteWithBorder.png'/>
                    </IconButton>   
                    <Link   className='btn dark-btn btn-space'
                            to={{
                                pathname: '/'
                            }}
                    >
                            <Typography variant="h6" >
                                Home
                            </Typography> 
                    </Link>
                     
                    <Typography variant="h6" >
                        About
                    </Typography> 
                    <Link   className='btn dark-btn btn-space'
                            to={{
                                pathname: '/videos'
                            }}
                    >
                            <Typography variant="h6" >
                                Videos
                            </Typography> 
                    </Link>
                    <Typography variant="h6" >
                        Photos
                    </Typography> 
                    <Typography variant="h6" >
                        Lyrics
                    </Typography> 
                    <Typography variant="h6" >
                        Store
                    </Typography> 
                    <Typography variant="h6" className={classes.title}>
                        Contact
                    </Typography>  
                    <div>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        edge="end"
                        onClick={handleDrawerOpen}
                        className={clsx(open && classes.hide)}
                    >
                        <QueueMusicIcon />
                    </IconButton>
                    </div>
                   
                </Toolbar>
               
            </AppBar>          
            <Drawer        
                classes={{paper: classes.drawerPaper}}   
                variant="persistent"
                anchor="right"
                open={open}
            >
                <div  className={classes.drawer}>
                    <div className={classes.drawerHeader}>
                        <IconButton onClick={handleDrawerClose}>
                            {theme.direction === 'rtl' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
                        </IconButton>
                    </div>
                    <div>
                        <SoundCloud/>
                    </div>
                </div>               
            </Drawer>
            <Toolbar id='back-to-top-anchor'/>  
        </React.Fragment>        
    )
}