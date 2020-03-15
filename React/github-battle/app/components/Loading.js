import React from 'react'
import PropTypes from 'prop-types'
const styles = {
    content: {
        fontSize: '35px',
        position: 'absolute',
        left: '0',
        right: '0',
        marginTop: '20px',
        textAlign: 'center',
    }
}
// Building Reusable Components
export default class Loading extends React.Component {
    state = { content: this.props.text }
    
    componentDidMount () {
        const {speed, text } = this.props
        //don't have to declare this.interval to access it later
        this.interval = window.setInterval(() => {
            this.state.content === text + '...'
            ? this.setState({ content: text})
            : this.setState(({content}) => ({ content: content + '.' }))
        }, speed)
    }

    componentWillUnmount () { window.clearInterval(this.interval) }

    render() {
       return(
            <p style={styles.content}>
                {this.state.content}
            </p> 
       ) 
    }
}

Loading.propTypes = {
    text: PropTypes.string.isRequired,
    speed: PropTypes.number.isRequired
}

Loading.defaultProps = {
    text: 'Loading',
    speed: 300
}