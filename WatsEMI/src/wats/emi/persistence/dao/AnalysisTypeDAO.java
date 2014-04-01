package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._AnalysisType;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:45
 * To change this template use File | Settings | File Templates.
 */
public class AnalysisTypeDAO extends GenericDAO<_AnalysisType> {
    public AnalysisTypeDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public AnalysisTypeDAO(GenericDAO dao) {
        super(dao);
    }
}
