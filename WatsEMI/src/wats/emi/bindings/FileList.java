package wats.emi.bindings;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "FileList")
@XmlType(name = "filelist")
public class FileList  implements Serializable {
	private static final long serialVersionUID = 7652180639958236012L;
	
	private List<FileDescription> files = new ArrayList<FileDescription>();
	
	public void addFile(FileDescription description) {
		files.add(description);
    }

	@XmlElement(name = "Description")
    public List<FileDescription> getFiles() {
        return files;
    }

}
