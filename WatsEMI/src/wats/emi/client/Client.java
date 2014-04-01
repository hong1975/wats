package wats.emi.client;

import java.util.List;
import java.util.concurrent.ConcurrentLinkedQueue;

import javax.servlet.http.HttpSession;

import wats.emi.bindings.Event;
import wats.emi.bindings.Events;

public class Client {
	private ConcurrentLinkedQueue<Event> eventQueue = new ConcurrentLinkedQueue<Event>();
	private HttpSession session;
	private String userId;
	private int eventId = 1;
	
	public Client(HttpSession session) {
		this.session = session;
	}
	
	public ConcurrentLinkedQueue<Event> getEventQueue() {
		return eventQueue;
	}
	
	public void addEvent(Event aEvent){
		eventQueue.add(aEvent);
    }
	
	public Events getEvents() {
		Event event;
        Events events = new Events();
        List<Event> eventList = events.getEvents();

        while ((event = eventQueue.poll()) != null) {
            event.setEventId(eventId++);

            // Reset eventId if max integer value is reached.
            if (eventId == Integer.MAX_VALUE) {
            	eventId = 1;
            }

            eventList.add(event);
        }

        if (!isNoEvent()) {
            events.setMoreEventsAvailable(true);
        }

        return events;
	}
	
	public boolean isNoEvent() {
        return eventQueue.isEmpty();
    }

	public String getUserId() {
		return userId;
	}


	public void setUserId(String userId) {
		this.userId = userId;
	}


	public HttpSession getSession() {
		return session;
	}

	public void setSession(HttpSession session) {
		this.session = session;
	}

}
