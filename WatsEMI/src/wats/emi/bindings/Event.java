package wats.emi.bindings;

import java.util.Date;
import java.io.Serializable;

import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlType;

@XmlType(name="event")
@XmlSeeAlso({
        //com.ericsson.simplewce.binding.P2PSessionEvent.class
})

public class Event implements Serializable {
	private static final long serialVersionUID = 2858766106737450000L;
	protected Date timestamp;
    protected int eventId;

    public Event() {
        timestamp = new Date();
    }

    @XmlAttribute
    public Date getTimestamp() {
        return timestamp;
    }

    @XmlAttribute
    public int getEventId() {
        return eventId;
    }

    public void setEventId(int eventId) {
        this.eventId = eventId;
    }
}