class EventEmitter {
    constructor() {
        this.events = {}
    }
  
    on(event, listener) {
        if (!this.events[event]) {
            this.events[event] = []
        }
    
        this.events[event].push(listener)
    }
  
    emit(event) {
        if (this.events[event]) {
            this.events[event].forEach(listener => {
                listener()
            })
        }
    }
}

const eventEmitter = new EventEmitter();

export default eventEmitter;