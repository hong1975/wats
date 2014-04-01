package wats.emi.bindings;

import java.util.ArrayList;
import java.util.List;
import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "Events")
@XmlType(name = "events")
public class Events
        implements Serializable {

	private static final long serialVersionUID = 3531438445648889070L;

    private Boolean moreEventsAvailable;

    @XmlElement(name = "Event", type = Event.class)
    protected List<Event> events = new ArrayList<Event>();

    public Events() {
    }

    @XmlElement(name= "HasMoreEvents")
    public boolean getMoreEventsAvailable() {
        return (moreEventsAvailable != null && moreEventsAvailable.equals(true));
    }

    public void setMoreEventsAvailable(boolean moreEventsAvailable) {
        if (moreEventsAvailable) {
            this.moreEventsAvailable = moreEventsAvailable;
        } else {
            this.moreEventsAvailable = null;
        }
    }

    public void addEvent(Event event) {
        events.add(event);
    }

    public List<Event> getEvents() {
        return events;
    }
}