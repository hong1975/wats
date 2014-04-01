package wats.emi.resource;

import com.sun.jersey.api.json.JSONConfiguration;
import com.sun.jersey.api.json.JSONJAXBContext;
import com.sun.jersey.json.impl.BaseJSONMarshaller;
import com.sun.jersey.json.impl.BaseJSONUnmarshaller;
import com.sun.xml.bind.v2.model.core.ID;

import javax.ws.rs.ext.ContextResolver;
import javax.ws.rs.ext.Provider;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.annotation.*;

import wats.emi.bindings.*;

import java.io.StringReader;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

/**
 *
 */
@Provider
public class JAXBContextResolver
        implements ContextResolver<JAXBContext> {
    private static final Logger logger = Logger.getLogger(JAXBContextResolver.class.getName());

    private JAXBContext context;
    private Class[] types = {
        AddTaskResult.class,
		Event.class,
		Events.class,
		FileDescription.class,
		FileList.class,
		GlobalRegion.class,
		ID.class,
		LimitSetting.class,
		Task.class,
		Tasks.class,
		Report.class,
		Reports.class,
		Region.class,
		Site.class,
		Sites.class,
		UpdateTaskRequest.class,
		UpdateTaskType.class,
		UpdateRegionRequest.class,
		UpdateRegionResult.class,
		UpdateRegionType.class,
		User.class,
		Users.class
    };

    @XmlRootElement(name = "Thing")
    @XmlType(name = "thing")
    public static class Thing {
        private List<String> name = new ArrayList<String>();

        @XmlElement(name = "name")
        public List<String> getName() {
            return name;
        }

        public void setName(List<String> name) {
            this.name = name;
        }
    }

    public JAXBContextResolver() throws Exception {
        logger.warning("JAXBContextResolver was called");
        this.context =
                new JSONJAXBContext(
                        JSONConfiguration.natural().build(), types);
        logger.warning("JAXBContextResolver was called finished");
    }

    @Override
    public JAXBContext getContext(Class<?> objectType) {
        for (Class type : types) {
            if (type == objectType) {
                return context;
            }
        }

        return null;
    }

    public static void main(String[] args) throws JAXBException {
        //JSONConfiguration config = JSONConfiguration.natural().build();
        //System.out.println("OK");

        final String json = "{\"name\":[]}";
        final JAXBContext jaxbContext = JSONJAXBContext.newInstance(Thing.class);
        System.out.println(json);
        final Thing thing = new BaseJSONUnmarshaller(jaxbContext,
                JSONConfiguration.natural().build())
                .unmarshalFromJSON(new StringReader(json), Thing.class);

        new BaseJSONMarshaller(jaxbContext,  JSONConfiguration.natural().build()).marshallToJSON(new Thing(), System.out);

        //System.out.println(thing);

    }
}