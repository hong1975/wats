package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._Site;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:47
 * To change this template use File | Settings | File Templates.
 */
public class SiteDAO extends GenericDAO<_Site> {
    public SiteDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public SiteDAO(GenericDAO dao) {
        super(dao);
    }
}
