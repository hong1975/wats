package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._EMIFile;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:46
 * To change this template use File | Settings | File Templates.
 */
public class EMIFileDAO extends GenericDAO<_EMIFile> {
    public EMIFileDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public EMIFileDAO(GenericDAO dao) {
        super(dao);
    }
}
